
function resetIdAEditar() {
    localStorage.setItem("idAEditar", null);
  }
  
  function loadTable() {
    resetIdAEditar();
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
            trHTML += '<button type="button" class="btn btn-outline-secondary" onclick="showFormEdicao(' + object['id'] + ')">Editar</button>';
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
  
  loadTable()
  
  function showUserCreateBox() {
    window.location.href = "objetivoForm.html"; // Replace "newpage.html" with the path to your desired HTML file
  }
  
  function showFormEdicao(id) {
    localStorage.setItem("idAEditar", id);
    window.location.href = "objetivoForm.html";
  }
  
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
        objetivoDelete(id);
      }
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
  
          trHTML += '<div class="popup-text">';
          trHTML += response.data;
          trHTML += '</div>';
  
          // Swal.fire({
          //   title: 'Metas',
          //   html: trHTML,
          //   width: '50%',
          // })
  
          Swal.fire({
            title: 'Metas',
            html: trHTML,
            confirmButtonText: 'OK',
            width: '50%',
            Height: '70%',
          });
  
          document.getElementById("view").innerHTML = trHTML;
        }
      })
      .catch((err) => {
        console.error
      })
  }
  