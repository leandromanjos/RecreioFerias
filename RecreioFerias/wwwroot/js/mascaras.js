document.addEventListener("DOMContentLoaded", function () {
    const inputs = document.querySelectorAll("[data-mask]");

    inputs.forEach(input => {
        input.addEventListener("input", function () {
            const mask = input.getAttribute("data-mask");
            const value = input.value.replace(/\D/g, "");
            let maskedValue = "";
            let index = 0;

            for (let char of mask) {
                if (char === "#" && index < value.length) {
                    maskedValue += value[index];
                    index++;
                } else if (char !== "#") {
                    maskedValue += char;
                }
            }

            input.value = maskedValue;
        });
    });
});

