<script>
    document.getElementById("select1").addEventListener("change", function () {
        const selectedValue = this.value;

    // Faz uma requisição ao servidor
    fetch(`/Home/GetSelect2Html?selectedValue=${selectedValue}`, {method: 'GET' })
            .then(response => response.text()) // Aqui obtemos o HTML como texto
            .then(html => {
                const select2 = document.getElementById("select2");
    select2.innerHTML = html; // Substitui o conteúdo do segundo select
            })
            .catch(error => console.error("Erro ao buscar opções:", error));
    });
</script>