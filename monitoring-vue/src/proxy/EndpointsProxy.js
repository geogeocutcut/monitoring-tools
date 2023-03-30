import axios from 'axios';

const apiUrl = 'https://localhost:7116'; // Remplacez cette URL par l'URL de votre API

export default {
  getAllApiEndpoints() {
    return axios.get(`${apiUrl}/ApiEndpoint`)
      .then(response => {
        const apiList = response.data;
        return apiList.map(api => {
          const id = api.id;
          const name = api.name;
          const description = api.description;
          const statusHttp = api.statusHttp;
          const information = api.information;
          const lastUpdated = new Date(api.lastUpdated);
          var ret =  { id:id, name:name, description:description, statusHttp:statusHttp, information:information, lastUpdated:lastUpdated };

          return ret;
        }).sort((a, b) => a.name.localeCompare(b.name));
      });
  },
};