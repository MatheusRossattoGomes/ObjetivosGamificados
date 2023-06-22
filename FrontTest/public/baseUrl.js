const baseUrl = 'http://localhost:5002',
  // const baseUrl = 'https://vomer.serveo.net',
  api = axios.create({
    baseURL: baseUrl + "/ObjetivosGamificados",
    headers: {
      'Content-type': 'application/json'
    }
  });

  apiAula = axios.create({
    baseURL: baseUrl + "/Aulas",
    headers: {
      'Content-type': 'application/json'
    }
  });

