
function resetIdAEditar() {
  localStorage.setItem("idAEditar", null);
}
const loadTable = () => {
  resetIdAEditar()
  apiAula.get("/GetAulas/" + 1)
    .then((response) => {
      if (response.status == 200) {
        var trHTML = '';
        const objects = response.data;
        for (let object of objects) {
          trHTML += '<tr>';
          trHTML += '<td>' + object['id'] + '</td>';
          trHTML += '<td>' + object['dataAulaString'] + '</td>';
          trHTML += '<td>' + object['descricao'] + '</td>';
          trHTML += '&ensp;';
          trHTML += '<td><button type="button" class="btn btn-outline-secondary" onclick="showFormEdicao(' + object['id'] + ')">Editar</button>';
          trHTML += '&ensp;';
          trHTML += '<button type="button" class="btn btn-outline-danger" onclick="showConfirmationDialog(' + object['id'] + ')">Deletar</button>';
          trHTML += '&ensp;';
          trHTML += '<button type="button" class="btn btn-outline-primary" onclick="imprimirAula(' + object['id'] + ')">Imprimir</button></td>';
          trHTML += "</tr>";
        }
        document.getElementById("mytable").innerHTML = trHTML;
      }
    })
    .catch((err) => {
      console.error("ops! ocorreu um erro" + err);
    });
}

document.addEventListener("DOMContentLoaded", function () {
  // Make the Axios GET request to fetch aulas data
  loadTable();
});

function showConfirmationDialog(id) {
  Swal.fire({
    text: 'Deseja mesmo deletar o registro?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Sim, deletar!',
    cancelButtonText: 'Cancelar'
  }).then((result) => {
    if (result.isConfirmed) {
      aulaDelete(id);
    }
  });
}

function aulaDelete(id) {
  apiAula.delete('/DeleteAula?id=' + id).then(response => {
    if (response.status == 200) {
      loadTable();
    }
  })
}

function showFormEdicao(id) {
  localStorage.setItem("idAEditar", id);
  window.location.href = "aulaForm.html";
}



function imprimirAula(id) {
  // Make an Axios GET request to retrieve class data
  apiAula.get("/GetAula/" + id)
    .then(function (response) {
      const classData = response.data;
      const classDataHtml = getClassDataHtml(classData);

      // Create a new window for printing
      const printWindow = window.open('', '_blank');
      printWindow.document.open();
      printWindow.document.write(classDataHtml);
      printWindow.document.close();

      // Print the document
      printWindow.print();
    })
    .catch(function (error) {
      console.log(error);
    });
}
// Function to convert tipoObjetivo to text
function convertTipoObjetivoParaTexto(tipo) {
  let descricaoTipo = "";
  switch (tipo + "") {
    case "-1":
      descricaoTipo = "Texto";
      break;
    case "-2":
      descricaoTipo = "Vídeo";
      break;
    case "-3":
      descricaoTipo = "Questionário";
      break;
  }
  return descricaoTipo;
}
// Function to generate the HTML content with class data
function getClassDataHtml(classData) {
  // Function to convert tipoObjetivo to icon class
  function convertTipoObjetivoParaIconClass(tipo) {
    let iconClass = "";
    switch (tipo + "") {
      case "-1":
        iconClass = "fa-file-alt";
        break;
      case "-2":
        iconClass = "fa-video";
        break;
      case "-3":
        iconClass = "fa-question-circle";
        break;
    }
    return iconClass;
  }

  // Generate the table rows for objectives
  var objectivesTableRows = classData.objetivos.map(function (objective) {
    const tipoObjetivoIconClass = convertTipoObjetivoParaIconClass(objective.tipoObjetivo);
    return `
      <tr>
        <td>
          <i class="fas ${tipoObjetivoIconClass} objective-icon"></i>
          ${objective.descricao}
        </td>
        <td>${objective.quantidade}</td>
        <td>${convertTipoObjetivoParaTexto(objective.tipoObjetivo)}</td>
      </tr>
    `;
  }).join('');

  // Generate the complete HTML content
  return `
    <!DOCTYPE html>
    <html>
    <head>
      <title>Informações da Aula</title>
      <!-- Bootstrap CSS -->
      <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
      <!-- Font Awesome Icons -->
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
      <style>
        /* Custom CSS styles */
        body {
          background-color: #f8f9fa;
          font-family: Arial, sans-serif;
        }
        
        .container {
          max-width: 800px;
        }
        
        .mt-5 {
          margin-top: 3rem !important;
        }
        
        .objective-icon {
          margin-right: 10px;
        }
        
        h1 {
          font-size: 2.5rem;
          color: #333;
        }
        
        h2 {
          font-size: 2rem;
          color: #333;
          margin-top: 2rem;
          margin-bottom: 1rem;
        }
        
        p {
          color: #555;
          font-size: 1.2rem;
          line-height: 1.5;
          margin-bottom: 2rem;
        }
        
        .table {
          margin-bottom: 3rem;
        }
        
        .table th {
          background-color: #f8f9fa;
          color: #333;
          font-size: 1.2rem;
          font-weight: bold;
        }
        
        .table td {
          vertical-align: middle;
          font-size: 1.1rem;
        }
        
        .fa-file-alt {
          color: #007bff;
        }
        
        .fa-video {
          color: #17a2b8;
        }
        
        .fa-question-circle {
          color: #ffc107;
        }
      </style>
    </head>
    <body>
      <div class="container mt-5">
        <h1>Informações da Aula - ${classData.descricao}</h1>
        <p>
          <strong>Descrição:</strong> ${classData.descricao}
        </p>
        <p>
          <strong>Data:</strong> ${classData.dataAula.slice(0, 10)}
        </p>

        <h2>Objetivos</h2>
        <table class="table">
          <thead>
            <tr>
              <th>Descrição</th>
              <th>Quantidade</th>
              <th>Tipo de Objetivo</th>
            </tr>
          </thead>
          <tbody>
            ${objectivesTableRows}
          </tbody>
        </table>
      </div>

      <!-- Bootstrap and Font Awesome JS scripts -->
      <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
      <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
      <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    </body>
    </html>
  `;
}












