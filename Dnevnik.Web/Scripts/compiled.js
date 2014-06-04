var chunk_Module;
(function (chunk_Module) {
    var chunk = (function () {
        function chunk() {
        }
        return chunk;
    })();

    define("chunk", function () {
    });
})(chunk_Module || (chunk_Module = {}));
//# sourceMappingURL=chunk.js.map
 var M;
(function (M) {
    var item = (function () {
        function item() {
        }
        return item;
    })();

    define("item/item", function () {
    });
})(M || (M = {}));
//# sourceMappingURL=item.js.map
 /// <reference path="../../Scripts/typings/requirejs/require.d.ts"/>
var MenuModule;
(function (MenuModule) {
    var Menu = (function () {
        function Menu(items) {
            this._handlers = {};
            this._items = Array.prototype.slice.call(items);
        }
        Menu.prototype.select = function (item) {
            if (!item || (this._items.indexOf(item) === -1)) {
                throw {
                    name: "Error",
                    message: "Item element should exist in elements collection"
                };
            }
            this._selected = item;
            this._handlers["select"].forEach(function (handler) {
                handler.apply(item);
            });
        };

        Menu.prototype.on = function (key, handler) {
            if (!this._handlers[key]) {
                this._handlers[key] = [];
            }
            this._handlers[key].push(handler);
        };
        return Menu;
    })();

    define("menu", function () {
        var elements = document.getElementsByClassName('menu__item__link');

        var menu = new Menu(elements);
        var i, max, elt, activeClass, active;

        for (i = 0, max = elements.length; i < max; i += 1) {
            elt = elements[i];
            elt.addEventListener('mouseover', function () {
                menu.select(this);
            }, false);
        }

        menu.on('select', function () {
            activeClass = 'menu__item__submenu__layout_active';
            active = document.getElementsByClassName(activeClass);

            for (i = 0, max = active.length; i < max; i += 1) {
                active[i].className = active[i].className.replace(activeClass, '');
            }
            for (i = 0, max = this.parentNode.childNodes.length; i < max; i += 1) {
                elt = this.parentNode.childNodes[i];
                if (elt.className && elt.className.indexOf('menu__item__submenu__layout') > -1) {
                    elt.className += ' ' + activeClass;
                }
            }
        });
    });
})(MenuModule || (MenuModule = {}));
//# sourceMappingURL=menu.js.map
 define("search", function () {
    var elts = document.getElementsByClassName('search__form__text');
    for (var i = 0, max = elts.length; i < max; i += 1) {
        elts[i].addEventListener('input', function () {
        }, false);
    }
});
//# sourceMappingURL=search.js.map
 var Links = (function () {
    function Links() {
        alert("asd");
    }
    return Links;
})();
 var someshit = (function () {
    function someshit() {
    }
    return someshit;
})();
//# sourceMappingURL=someshit.js.map
