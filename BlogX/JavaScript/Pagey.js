//first pass will be vanilla js.
var buttonNext = document.querySelector(".next");
var buttonPrev = document.querySelector(".prev");
var page = document.querySelector("#page");

buttonNext.addEventListener("click", function (e) {
        var x = new XMLHttpRequest();
        x.open("GET", "/blog/index?page=" + (pageNo+1) + "&json=true");
        x.onreadystatechange = function (e) {
            if (this.readyState == 4 && this.status == 200)
            {                
                var json = JSON.parse(this.responseText);
                btnClasses(json);
                updatePage(json);
                pageNo++;
                history.pushState({}, "title", window.location.pathname + "?page=" + pageNo);
            }
        }
        x.send();
        e.preventDefault();
    }
);

buttonPrev.addEventListener("click", function (e) {
    var x = new XMLHttpRequest();
    x.open("GET", "/blog/index?page=" + (pageNo-1) + "&json=true");
    x.onreadystatechange = function (e) {
        if (this.readyState == 4 && this.status == 200) {
            var json = JSON.parse(this.responseText);
            btnClasses(json);
            updatePage(json);
            pageNo--;
            history.pushState({}, "title", window.location.pathname + "?page=" + pageNo);
           
        }
    }
    x.send();
    e.preventDefault();
});

function btnClasses(json)
{
    if (json.hasNext) {
        buttonNext.classList.add("enabled");
    }
    else buttonNext.classList.remove("enabled");
    if (json.hasPrev) {
        buttonPrev.classList.add("enabled");
    }
    else buttonPrev.classList.remove("enabled");
}

function updatePage(json)
{
    page.innerHTML = "";
    for (var i = 0; i < json.results.length; i++) {
        var result = json.results[i];
        var toAdd = document.createElement("div");
        var link = document.createElement("a");
        link.text = result.Title;
        link.href = "/blog/detail/" + result.Id;
        var p = document.createElement("p");
        p.innerText = result.Content;
        toAdd.appendChild(link);
        toAdd.appendChild(p);
        page.appendChild(toAdd);
    }

}