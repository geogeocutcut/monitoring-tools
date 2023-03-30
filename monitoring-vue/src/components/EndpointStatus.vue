<template>
    <div class="circle" :class="status"></div>
</template>

<script>

export default {
  props: {
    statusHttp: {
      required: true
    }
  },
  computed: {
    status() {
        if( this.statusHttp<400)
          return 'green';
        else
          return 'red';
    }
  }
};
</script>

<style scoped>

.circle {
  width: 20px;
  height: 20px;
  border-radius: 50%;
  margin-right: 10px;
}

.green {
  background-color: green;
}

.orange {
  background-color: orange;
}

.red {
  background-color: red;
}

.grey {
  background-color: grey;
}
</style>