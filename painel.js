// Tickets fictícios para exibição
const tickets = Array.from({ length: 20 }, (_, i) => ({
    id: i + 1,
    user: `Usuário ${Math.floor(Math.random() * 1000)}`,
    description: `Descrição do ticket ${i + 1}`,
    responsible: `Responsável ${Math.floor(Math.random() * 10)}`,
    creator: `Criador ${Math.floor(Math.random() * 10)}`
}));

let currentPage = 1;
let ticketsPerPage = 10;

const ticketsBody = document.getElementById("ticketsBody");
const ticketsPerPageSelect = document.getElementById("ticketsPerPage");

function saudacao() {
    const nome = "João Silva";
    const horaAtual = new Date().getHours(); // Obtém a hora atual (0-23)
    let saudacao;

    if (horaAtual >= 5 && horaAtual < 12) {
        saudacao = "Bom dia";
    } else if (horaAtual >= 12 && horaAtual < 18) {
        saudacao = "Boa tarde";
    } else {
        saudacao = "Boa noite";
    }

    return `${saudacao}, ${nome}!`;
}

document.addEventListener("DOMContentLoaded", () => {
    const saudacaoElemento = document.getElementById("saudacao");
    saudacaoElemento.textContent = saudacao(); // Insere a saudação no elemento
});

function renderTickets() {
    ticketsBody.innerHTML = "";
    const start = (currentPage - 1) * ticketsPerPage;
    const end = start + ticketsPerPage;
    const pageTickets = tickets.slice(start, end);

    pageTickets.forEach(ticket => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${ticket.id}</td>
            <td>${ticket.user}</td>
            <td>${ticket.description}</td>
            <td>${ticket.responsible}</td>
            <td>${ticket.creator}</td>
        `;
        ticketsBody.appendChild(row);
    });
}

document.getElementById("prevPage").addEventListener("click", () => {
    if (currentPage > 1) {
        currentPage--;
        renderTickets();
    }
});

document.getElementById("nextPage").addEventListener("click", () => {
    if (currentPage < Math.ceil(tickets.length / ticketsPerPage)) {
        currentPage++;
        renderTickets();
    }
});

ticketsPerPageSelect.addEventListener("change", (e) => {
    ticketsPerPage = parseInt(e.target.value);
    currentPage = 1;
    renderTickets();
});

// Inicialização
renderTickets();
