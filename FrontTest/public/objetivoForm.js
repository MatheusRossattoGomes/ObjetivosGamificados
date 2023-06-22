
const selectElement = document.getElementById("tipoObjetivo");

function alteraLabelQuantidade(value) {
  let labelQuantidade = "";
  switch (value) {
    case "-1":
      labelQuantidade = "Quantidade de páginas"
      break;
    case "-2":
      labelQuantidade = "Quantidade de minutos"
      break;
    case "-3":
      labelQuantidade = "Quantidade de questões"
      break;
  }

  document.getElementById('labelQuantidade').innerHTML = labelQuantidade;
}

selectElement.addEventListener("change", (event) => {
  alteraLabelQuantidade(event.target.value);
});

function validateForm(callback) {
  var descricao = document.getElementById("descricao").value;
  var dataEntrega = document.getElementById("dataEntrega").value;
  var quantidade = document.getElementById("quantidade").value;

  // Check if any field is blank
  if (descricao === "" || dataEntrega === "" || quantidade === "") {
    alert("Todos os campos são obrigatórios");
    return false;
  }

  // Check if quantidade is a positive number
  if (quantidade <= 0) {
    alert("A quantidade deve ser um número positivo");
    return false;
  }

  // Check if creation date is today or later
  var today = new Date().toISOString().split("T")[0];
  if (dataEntrega < today) {
    alert("A data de entrega não pode ser anterior à data atual");
    return false;
  }

  callback();
  return true;
}


function saveObjetivo(event) {
  event.preventDefault();
  validateForm(saveFunction);
}

function saveFunction (){
  const id = document.getElementById("id").value;
  const descricao = document.getElementById("descricao").value;
  const dataEntrega = document.getElementById("dataEntrega").value;
  const quantidade = document.getElementById("quantidade").value;
  const tipoObjetivo = document.getElementById("tipoObjetivo").value;
  let dadosJson = {
    "Id": id,
    "Descricao": descricao,
    "DataEntrega": dataEntrega,
    "Quantidade": quantidade,
    "TipoObjetivo": tipoObjetivo,
    "IdUsuario": 1,
    "Objetivo": ""
  };

  if (id)
    alterarObjetivo(dadosJson);
  else
    addObjetivo(dadosJson);
}

function addObjetivo(dadosJson) {
  api.post('/AddObjetivo?', JSON.stringify(dadosJson))
    .then(function (response) {
      if (response.status == 200) {
        window.location.href = "objetivos.html";
      }
    })
    .catch(function (error) {
      console.log(error);
    });
}

function alterarObjetivo(dadosJson) {
  api.put(`/AlterarObjetivo/`, JSON.stringify(dadosJson))
    .then(function (response) {
      if (response.status == 200) {
        window.location.href = "objetivos.html";
      }
    })
    .catch(function (error) {
      console.log(error);
    });
}

function getObjetivo() {
  let idAEditar = localStorage.getItem("idAEditar");
  if (idAEditar != "null") {
    api.get("/GetObjetivo/" + idAEditar)
      .then((response) => {
        const objetivo = response.data;
        document.getElementById("id").value = idAEditar;
        document.getElementById("descricao").value = objetivo.descricao;
        document.getElementById("dataEntrega").value = objetivo.dataEntrega.slice(0, 10);
        document.getElementById("tipoObjetivo").value = objetivo.tipoObjetivo;
        document.getElementById("quantidade").value = objetivo.quantidade;
      })
      .catch((err) => {
        console.error();
      })
  }

  alteraLabelQuantidade(document.getElementById("tipoObjetivo").value);

}


