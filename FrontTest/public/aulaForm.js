
const selectElement = document.getElementById("tipoInput");
let createObjetivoModal;
document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("#aulaForm");
    alteraLabelQuantidade(selectElement.value);

    form.addEventListener("submit", function (event) {
        event.preventDefault();
        validateForm(saveFunction);
    });

    function validateForm(callback) {
        const id = document.querySelector("#id").value;
        const descricao = document.querySelector("#descricaoInput").value;
        const data = document.querySelector("#dataInput").value;
        const resumo = document.querySelector("#resumoInput").value;
        var dados = { id, descricao, data, resumo };

        if (descricao.trim() === "" || data.trim() === "" || resumo.trim() === "") {
            alert("Preencha todos os dados.");
            return;
        }

        const yesterday = new Date();
        yesterday.setDate(yesterday.getDate() - 1);
        const selectedDate = new Date(data);

        if (selectedDate <= yesterday) {
            alert("Selecione uma data futura para proseguir.");
            return;
        }

        callback(dados);
        return true;
    }

    function saveFunction(dados) {
        // Get the HTML table element
        const table = document.getElementById("objetivosList");

        // Get the table headers
        const headers = Array.from(table.querySelectorAll("th")).map((th) => th.getAttribute("name"));

        // Get all table rows (excluding the header row)
        const rows = Array.from(table.querySelectorAll("tr")).slice(1);

        // Convert each row into an object
        const data = rows.map((row) => {
            const rowData = Array.from(row.querySelectorAll("td")).map((td) => td.textContent);
            const obj = {};
            headers.forEach((header, index) => {
                obj[header] = rowData[index];
            });
            return obj;
        });

        data.forEach(x => {
            x.Objetivo = "";
            x.TipoObjetivo = convertTipoObjetivoParaId(x.TipoObjetivo)
        });

        let dadosJson = {
            "Id": dados.id,
            "Descricao": dados.descricao,
            "DataAula": dados.data,
            "Resumo": dados.resumo,
            "IdUsuario": 1,
            "Objetivos": data
        };

        if (dados.id)
            alterarAula(dadosJson);
        else
            addAula(dadosJson);
    }

    function addAula(dadosJson) {
        apiAula.post('/AddAula/', JSON.stringify(dadosJson))
            .then(function (response) {
                if (response.status == 200) {
                    window.location.href = "aulas.html";
                }
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function alterarAula(dadosJson) {
        apiAula.put(`/AlterarAula/`, JSON.stringify(dadosJson))
            .then(function (response) {
                if (response.status == 200) {
                    window.location.href = "aulas.html";
                }
            })
            .catch(function (error) {
                console.log(error);
            });
    }
});

function getAula() {
    let idAEditar = localStorage.getItem("idAEditar");
    if (idAEditar != "null") {
        apiAula.get("/GetAula/" + idAEditar)
            .then((response) => {
                const aula = response.data;
                document.querySelector("#id").value = idAEditar;
                document.querySelector("#descricaoInput").value = aula.descricao;
                document.querySelector("#dataInput").value = aula.dataAula.slice(0, 10);
                document.querySelector("#resumoInput").value = aula.resumo;
                aula.objetivos.forEach(x => addObjetivos(x));
            })
            .catch((err) => {
                console.error();
            })
    }
}
function addObjetivos(objetivo) {

    const newRow = `
        <tr>
          <td>${objetivo.id}</td>
          <td>${objetivo.descricao}</td>
          <td>${objetivo.dataCriacao?.slice(0, 10) ?? ""}</td>
          <td>${objetivo.quantidade}</td>
          <td>${convertTipoObjetivoParaTexto(objetivo.tipoObjetivo)}</td>
          <td>
         <button type="button" class="btn btn-outline-danger" onclick="showConfirmationDialog(this)">Deletar</button></td>  
        </tr>
      `;

    document.querySelector("#objetivosList tbody").insertAdjacentHTML("beforeend", newRow);

}
getAula();


document.getElementById("criarObjetivoBtn").addEventListener("click", function () {
    createObjetivoModal = new bootstrap.Modal(document.getElementById("createObjetivoModal"));
    createObjetivoModal.show();
});

document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("#objetivoForm");

    form.addEventListener("load", function (event) {
        event.preventDefault();
        validateModal(addLinha);
    });

    form.addEventListener("submit", function (event) {
        event.preventDefault();
        validateModal(addLinha);
    });
});

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

function convertTipoObjetivoParaId(tipo) {

    let idTipo = "";
    switch (tipo) {
        case "Texto":
            idTipo = -1;
            break;
        case "Vídeo":
            idTipo = -2;
            break;
        case "Questionário":
            idTipo = -3;
            break;
    }
    return idTipo;
}

function addLinha() {
    const objetivoId = document.getElementById("idObjetivo").value,
     descricao = document.getElementById("descricaoObjetivoInput").value,
     quantidade = document.getElementById("quantidadeInput").value,
     tipo = document.getElementById("tipoInput").value;
    let objetivo = {
            id: objetivoId,
            descricao: descricao,
            dataCriacao: null,
            quantidade: quantidade,
            tipoObjetivo: tipo
        };

    addObjetivos(objetivo)

    // Close the modal
    fecharModal();
}


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

function validateModal(callback) {
    var descricao = document.getElementById("descricaoObjetivoInput").value;
    var quantidade = document.getElementById("quantidadeInput").value;

    // Check if any field is blank
    if (descricao === "" || quantidade === "") {
        alert("Todos os campos são obrigatórios");
        return false;
    }

    // Check if quantidade is a positive number
    if (quantidade <= 0) {
        alert("A quantidade deve ser um número positivo");
        return false;
    }

    callback();
    return true;
}

function fecharModal() {
    const objetivoForm = document.getElementById("objetivoForm");
    objetivoForm.reset();
    createObjetivoModal.hide();
}

function showConfirmationDialog(btn) {
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
        objDelete(btn.parentElement.parentElement.rowIndex);
    }
  });
}

function objDelete(id) {
    document.getElementById(
        'objetivosList').deleteRow(id)
}


