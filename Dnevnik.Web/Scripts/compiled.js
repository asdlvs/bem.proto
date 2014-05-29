/**
* Created by vlebedev on 22.05.2014.
*/
 /// <reference path="../../Scripts/typings/requirejs/require.d.ts"/>
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

define("menu/menu", function () {
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
//# sourceMappingURL=menu.js.map
