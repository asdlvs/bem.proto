
define("search", ()=> {
    var elts = document.getElementsByClassName('search__form__text');
    for (var i = 0, max = elts.length; i < max; i += 1) {
        elts[i].addEventListener('input', ()=> {
            
        }, false);
    }
}); 