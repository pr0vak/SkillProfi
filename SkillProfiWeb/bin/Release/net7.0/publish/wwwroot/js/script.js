let inquiry = document.querySelector('.inquiry')
let btnInquiry = document.querySelector('.hero__btn')

btnInquiry.addEventListener('click', function() {
    inquiry.style.opacity = 1
    inquiry.style.transform = 'translateY(0)'
    inquiry.style.pointerEvents = 'inherit'
})