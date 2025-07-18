const apiBaseURL = "https://projetoapigenio-fmf4f6b8gpduc8ct.brazilsouth-01.azurewebsites.net";

function ExibirGenio(){
    fetch(`${apiBaseURL}/Genio/Exibir`)
        .then(response => {
            return response.json()
        })
        .then(genios => {
            const container = document.getElementById('mostrarExibir');
            container.innerHTML = ''; 

            genios.forEach(genio => {
                container.innerHTML += `
                    <div class="card mt-3">
                        <div class="card-body">
                            <h5><strong>ID: </strong> ${genio.genioID}</h5>
                            <p><strong>Nome: </strong> ${genio.genioNome}</p>
                            <p><strong>Ano de Nascimento: </strong> ${genio.anoNascimento}</p>
                            <p><strong>Ano de Óbito: </strong> ${genio.anoObito}</p>
                            <p><strong>Descrição: </strong> ${genio.descricao}</p>
                            <p><strong>Contribuições: </strong> ${genio.contribuicoes}</p>
                        </div>
                    </div>
                `;
            });
        })  
}

function ConsultarGenio(){
    const ano = document.getElementById('inputAno').value;
    if(!ano){
        alert("Preencha o ano!");
        //document.getElementById('alertaEncontrar').innerHTML = `<p>Preencha o ano!</p>`;
        return;
    }else if(ano>2024){
        alert("Ano maior que o atual!");
        return;
    }
    fetch(`${apiBaseURL}/Genio/Procurar/${ano}`)
        .then(response => {
            return response.json()
        })
        .then(genio => {
            document.getElementById('mostrarEncontrar').innerHTML =
            `<h5>Gênio encontrado: </h5>
            <p><strong>ID: </strong> ${genio.genioID}</p>
            <p><strong>Nome: </strong> ${genio.genioNome}</p>
            <p><strong>Nascimento: </strong> ${genio.anoNascimento}</p>
            <p><strong>Óbito: </strong> ${genio.anoObito}</p>
            <p><strong>Descrição: </strong> ${genio.descricao}</p>
            <p><strong>Contruibuições: </strong> ${genio.contribuicoes}</p>`
        })
        .catch(error =>
            console.error('Error: ', error)
        );
}

function AdicionarGenio(){

    const nome = document.getElementById('inputAddNome').value;
    const anoNascimento = document.getElementById('inputAddNascimento').value;
    const anoObito = document.getElementById('inputAddObito').value;
    const descricao = document.getElementById('inputAddDescricao').value;
    const contribuicoes = document.getElementById('inputAddContribuicoes').value;

    if(!nome){
        alert("Preencha o nome corretamente!!");
        return;
    }else if(!anoNascimento || anoNascimento>2024){
        alert("Preencha o ano de nascimento corretamente!");
        return;
    }else if(!anoObito || anoObito>2024){
        alert("Preencha o ano de óbito corretamente!!");
        return;
    }else if(!descricao){
        alert("Preencha a descrição corretamente!!");
        return;
    }else if(!contribuicoes){
        alert("Preencha as contribuições corretamente!!");
        return;
    }

    const novoGenio = {
        GenioNome: nome,
        AnoNascimento: parseInt(anoNascimento),
        AnoObito: parseInt(anoObito),
        Descricao: descricao,
        Contribuicoes: contribuicoes
    };

    fetch(`${apiBaseURL}/Genio/Adicionar`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(novoGenio)
    })

    .then(response => {
        return response.json();
    })
    .then(genioAdicionado => {
        document.getElementById('mostrarAdicionar').innerHTML = 
        `<p>Gênio adicionado com sucesso!</p>`;
    })
    
    .catch(error => {
        document.getElementById('mostrarAdicionar').innerHTML = `<p>Erro ao adicionar gênio</p>`;
    });
}

function AtualizarGenio(){

    const id = document.getElementById('inputAttId').value;
    const nome = document.getElementById('inputAttNome').value;
    const anoNascimento = document.getElementById('inputAttAnoNascimento').value;
    const anoObito = document.getElementById('inputAttAnoObito').value;
    const descricao = document.getElementById('inputAttDescricao').value;
    const contribuicoes = document.getElementById('inputAttContribuicoes').value;

    if(!nome){
        alert("Preencha o nome corretamente!!");
        return;
    }else if(!anoNascimento || anoNascimento>2024){
        alert("Preencha o ano de nascimento corretamente!");
        return;
    }else if(!anoObito || anoObito>2024 || anoObito<anoNascimento){
        alert("Preencha o ano de óbito corretamente!!");
        return;
    }else if(!descricao){
        alert("Preencha a descrição corretamente!!");
        return;
    }else if(!contribuicoes){
        alert("Preencha as contribuições corretamente!!");
        return;
    }

    const genioAtualizado = {
        GenioNome: nome,
        AnoNascimento: parseInt(anoNascimento),
        AnoObito: parseInt(anoObito),
        Descricao: descricao,
        Contribuicoes: contribuicoes
    };

    fetch(`${apiBaseURL}/Genio/Atualizar/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(genioAtualizado)
    })

    .then(response => {
        return response.json();
    })

    .then(genio => {
        document.getElementById('mostrarAtualizar').innerHTML = 
        `<p>Gênio atualizado com sucesso!</p>`;
    })

    .catch(error => {
        document.getElementById('mostrarAtualizar').innerHTML = `<p>Erro ao atualizar gênio</p>`;
    });

}

function DeletarGenio(){
    const id = document.getElementById('inputDelId').value;
    
    if(!id){
        alert("Preencha o ID!");
    }

    fetch(`${apiBaseURL}/Genio/Deletar/${id}`, {
        method: 'DELETE',
    })
    .then(response => {
        if (response.status === 204) {
            document.getElementById('mostrarDeletar').innerHTML = `<p>Deletado com sucesso!</p>`;
        } else if (response.status === 404) {
            document.getElementById('mostrarDeletar').innerHTML = `<p>ID não encontrado!</p>`;
        }
    })
    .catch(error => {
        console.error('Error: ', error)
    });
}