




<template>
  <div class="app">
    <div class="api-list">
      <div v-if="loading" class="loading">Chargement en cours...</div>
      <div v-else-if="endpointsList.length === 0" class="empty">Aucune API trouvée.</div>
      <div v-else>
        <vue-good-table
          :columns="columns"
          :rows="endpointsList"
          styleClass="vgt-table condensed"
          theme="nocturnal">
          <template #table-row="props">
            <span
              class="wrap"
              v-if="props.column.field == 'statusHttp'">
              <endpoint-status :statusHttp="props.row.statusHttp"></endpoint-status>
            </span>
            <span v-else>
              {{props.formattedRow[props.column.field]}}
            </span>
          </template>
        </vue-good-table>
        
      </div>
    </div>
  </div>
</template>

<script>
import EndpointsProxy from './proxy/EndpointsProxy';
import EndpointStatus from './components/EndpointStatus.vue';
// import 'vue-good-table/dist/vue-good-table.css';
// import VueGoodTable from 'vue-good-table';
// import HelloWorld from './components/HelloWorld.vue';
import 'vue-good-table-next/dist/vue-good-table-next.css'
import { VueGoodTable } from 'vue-good-table-next';



export default {
  components: {
    'vue-good-table':VueGoodTable,
    'endpoint-status':EndpointStatus
    //'hello-world':HelloWorld
  },
  data() {
    return {
      endpointsList: [],
      loading: true,
      timer: '',
      columns: [
        {
          label: '',
          field: 'statusHttp',
          width: '10px',
          sortable:false
        },
        {
          label: 'Name',
          field: 'name',
          width: '50px',
          sortable:false
        },
        {
          label: 'Description',
          field: 'description',
          width: '200px',
          sortable:false
        },
        {
          label: 'Dernier Message',
          field: 'lastUpdated',
          width: '150px',
          formatFn: this.lastUpdatedFormat,
          sortable:false
        },
        {
          label: 'Message',
          field: this.informationField,
          sortable:false
        },
      ],
    };
  },
  mounted() {
    this.fetchApiEndpoints(true);
    
    this.timer = setInterval(this.fetchApiEndpoints, 60000);
  },
  methods:{
    informationField(rowObj) {
      return rowObj.statusHttp+" : "+rowObj.information ;
    },
    lastUpdatedFormat(value) {
      const date = new Date(value)
      return date.toLocaleDateString()+" "+date.toLocaleTimeString() ;
    },
    async fetchApiEndpoints(loadManagement=false) {
      if(loadManagement)
        this.loading = true;
      try {
        const data = await EndpointsProxy.getAllApiEndpoints();
        const apiList = data.map((api) => {
          return { ...api };
        });
        this.endpointsList = apiList;
      } catch (error) {
        console.error(error);
      } finally {
        this.loading = false;
      }
    },
    cancelAutoUpdate () {
        clearInterval(this.timer);
    }
  },
  unmounted () {
    this.cancelAutoUpdate();
  }
};
</script>

<style scoped>
.app {
  margin: 0 auto;
  padding: 20px;
  font-family: 'Roboto', sans-serif;
}

.loading {
  text-align: center;
  font-style: italic;
  color: grey;
}

.empty {
  text-align: center;
  font-style: italic;
  color: grey;
}

</style>