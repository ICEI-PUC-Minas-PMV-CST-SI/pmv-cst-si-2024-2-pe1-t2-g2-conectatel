document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Prevenir o envio do formulário padrão

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const errorMessage = document.getElementById("error-message");

    if (username === "admin" && password === "123") {
        window.location.href = "painel.html"; // Redirecionar para painel.html
    } else {
        errorMessage.textContent = "Usuário ou senha incorretos.";
    }
});
