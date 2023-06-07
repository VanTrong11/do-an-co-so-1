const allStarts = document.querySelectorAll('.star');
const totalStarInput = document.getElementById('action_total_star')

let current_rating = document.querySelector('.current_rating');

allStarts.forEach((star, i) => {
    star.onclick = function () {
        let currcent_star_level = i + 1;
        current_rating.innerText = `${currcent_star_level} / 5`;

        totalStarInput.value = currcent_star_level

        allStarts.forEach((star, j) => {

            if (currcent_star_level >= j + 1) {
                star.innerHTML = '&#9733';
            } else {
                star.innerHTML = '&#9734';
            }
        })
    }
})