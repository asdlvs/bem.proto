/// <reference path="../../Scripts/typings/requirejs/require.d.ts"/>
class Menu {
    _items: HTMLElement[];
    _selected: HTMLElement;
    _handlers: { [key: string]: Function[] } = {};

    constructor(items: HTMLElement[]) {
        this._items = Array.prototype.slice.call(items);
    }

    select(item) {
        if (!item || (this._items.indexOf(item) === -1)) {
            throw {
                name: "Error",
                message: "Item element should exist in elements collection"
            };
        }
        this._selected = item;
        this._handlers["select"].forEach(handler=> {
            handler.apply(item);
        });
    }

    on(key: string, handler: (data?) => void) {
        if (!this._handlers[key]) {
            this._handlers[key] = [];
        }
        this._handlers[key].push(handler);
    }
}

define("menu/menu", () => {
    var elements = <HTMLElement[]><any>document.getElementsByClassName('menu__item__link');

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
        active = <HTMLElement[]><any>document.getElementsByClassName(activeClass);

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





