
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
          trHTML += '<button type="button" class="btn btn-outline-danger" onclick="showConfirmationDialog(' + object['id'] + ')">Deletar</button></td>';
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
