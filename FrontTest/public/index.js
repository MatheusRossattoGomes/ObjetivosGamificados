
// const api = axios.create({
//   baseURL: "http://localhost:5002/ObjetivosGamificados",
//   headers: {
//     'Content-type': 'application/json'
//   }
// });
// const baseUrl = 'http://localhost:5002',
  const baseUrl = 'https://vomer.serveo.net',
  api = axios.create({
    baseURL: baseUrl + "/ObjetivosGamificados",
    headers: {
      'Content-type': 'application/json'
    }
  });


function loadTable() {
  api.get("/GetObjetivos/" + 1)
    .then((response) => {
      if (response.status == 200) {
        var trHTML = '';
        const objects = response.data;
        for (let object of objects) {
          trHTML += '<tr>';
          trHTML += '<td>' + object['id'] + '</td>';
          trHTML += '<td>' + object['descricao'] + '</td>';
          trHTML += '<td>' + object['dataCriacaoString'] + '</td>';
          trHTML += '<td>' + object['dataEntregaString'] + '</td>';
          trHTML += '<td>' + object['quantidade'] + '</td>';
          trHTML += '<td>' + object['tipoObjetivoString'] + '</td>';
          trHTML += '<td><button type="button" class="btn btn-outline-primary" onclick="getViewObjetivo(' + object['id'] + ')">Metas</button>';
          trHTML += '&ensp;';
          trHTML += '<button type="button" class="btn btn-outline-secondary" onclick="showFormEdicaoUsuario(' + object['id'] + ')">Edit</button>';
          trHTML += '&ensp;';
          trHTML += '<button type="button" class="btn btn-outline-danger" onclick="objetivoDelete(' + object['id'] + ')">Deletar</button></td>';
          trHTML += "</tr>";
        }
        document.getElementById("mytable").innerHTML = trHTML;
      }
    })
    .catch((err) => {
      console.error("ops! ocorreu um erro" + err);
    });
}

loadTable();

function showUserCreateBox() {
  Swal.fire({
    title: 'Create user',
    html:
      '<input id="id" type="hidden">' +
      '<input id="descricao" class="swal2-input" placeholder="Descrição">' +
      '<input type="date" id="dataEntrega" class="swal2-input" placeholder="Data entrega">' +
      '<select class="swal2-input" id="tipoObjetivo" name="tipoObjetivo" size="3" placeholder="Tipo objetivo" >' +
      '<option value="-1">Texto</option>' +
      '<option value="-2">Vídeo</option>' +
      '<option value="-3">Questionário</option>' +
      '</select>' +
      '<input id="quantidade" class="swal2-input" placeholder="Quantidade">',
    focusConfirm: false,
    preConfirm: () => {
      addObjetivo();
    }
  })
}

function addObjetivo() {
  const descricao = document.getElementById("descricao").value;
  const dataEntrega = document.getElementById("dataEntrega").value;
  const quantidade = document.getElementById("quantidade").value;
  const tipoObjetivo = document.getElementById("tipoObjetivo").value;
  let dadosJson = {
    "Descricao": descricao,
    "DataEntrega": dataEntrega,
    "Quantidade": quantidade,
    "TipoObjetivo": tipoObjetivo,
    "IdUsuario": 1,
    "Objetivo": ""
  };

  api.post('/AddObjetivo?', JSON.stringify(dadosJson))
    .then(function (response) {
      if (response.status == 200) {
        loadTable();
      }
    })
    .catch(function (error) {
      console.log(error);
    });
}


function showFormEdicaoUsuario(id) {
  api.get("/GetObjetivo/" + id)
    .then((response) => {
      const objetivo = response.data;
      Swal.fire({
        title: 'Editar obejtivo',
        html:
          '<input id="id" type="hidden" value="' + objetivo.id + '">' +
          '<input id="descricao" class="swal2-input" placeholder="Descrição" value="' + objetivo.descricao + '">' +
          '<input type="date" id="dataEntrega" class="swal2-input" placeholder="Data entrega" value="' + objetivo.dataEntrega.slice(0, 10) + '">' +
          '<select class="swal2-input" id="tipoObjetivo" name="tipoObjetivo" size="3" placeholder="Tipo objetivo">' +
          '<option id="tipoObjetivo-1" "value="-1">Texto</option>' +
          '<option id="tipoObjetivo-2" value="-2">Vídeo</option>' +
          '<option id="tipoObjetivo-3" value="-3">Questionário</option>' +
          '</select>' +
          '<input id="quantidade" class="swal2-input" placeholder="Quantidade" value="' + objetivo.quantidade + '">',
        focusConfirm: false,
        preConfirm: () => {
          alterarObjetivo();
        }
      })
      document.getElementById("tipoObjetivo" + objetivo.tipoObjetivo).selected = "true";
    })
    .catch((err) => {
      console.error
    })
}

function alterarObjetivo() {
  const id = document.getElementById("id").value,
    descricao = document.getElementById("descricao").value,
    dataEntrega = document.getElementById("dataEntrega").value,
    tipoObjetivo = document.getElementById("tipoObjetivo").value,
    quantidade = document.getElementById("quantidade").value,
    values = { id, descricao, dataEntrega, tipoObjetivo, quantidade, objetivo: "" };

  api.put(`/AlterarObjetivo/`, JSON.stringify(values))
    .then(function (response) {
      if (response.status == 200) {
        loadTable();
      }
    })
    .catch(function (error) {
      console.log(error);
    });
}

function objetivoDelete(id) {
  api.delete('/DeleteObjetivo?id=' + id).then(response => {
    if (response.status == 200) {
      loadTable();
    }
  })
}

function getViewObjetivo(id) {

  api.get("/GetViewObjetivos/" + id)
    .then((response) => {
      if (response.status == 200) {
        var trHTML = '';

        trHTML += '<tr>';
        trHTML += '<td>' + response.data + '</td>';
        trHTML += "</tr>";

        Swal.fire({
          title: 'Metas',
          html: trHTML,
        })

        document.getElementById("view").innerHTML = trHTML;
      }
    })
    .catch((err) => {
      console.error
    })
}
