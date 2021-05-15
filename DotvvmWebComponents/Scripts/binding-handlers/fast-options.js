export function init() {
    ko.bindingHandlers["fast-options"] = {
        init(element, valueAccessor, allBindings, viewModel, bindingContext) {
            const value = ko.unwrap(valueAccessor()) || {};
        },
        update(element, valueAccessor, allBindings, viewModel, bindingContext) {
            const value = ko.unwrap(valueAccessor()) || {};

            if (Array.isArray(value)) {
                element.replaceChildren([]);
                for (let i of value) {
                    let opt = document.createElement("fluent-option");
                    opt.textContent = ko.unwrap(i);
                    element.appendChild(opt);
                }
            }
        }
    }
}
