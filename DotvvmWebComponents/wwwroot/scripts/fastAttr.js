import { Observable } from './fast/fast-components.min.js';

ko.bindingHandlers["fast-attr"] = {
    init(element, valueAccessor, allBindings, viewModel, bindingContext) {
        const notifier = Observable.getNotifier(element);
        const value = ko.unwrap(valueAccessor()) || {};
        ko.utils.objectForEach(value, function (attrName, attrValue) {
            notifier.subscribe({
                handleChange(source, propertyName) {
                    const expr = (valueAccessor() || {})[attrName];
                    if (ko.isWritableObservable(expr)) {
                        expr(source[propertyName]);
                    }
                }
            }, attrName);
        });
    },
    update(element, valueAccessor, allBindings, viewModel, bindingContext) {
        ko.bindingHandlers['attr']['update'](element, valueAccessor, allBindings, viewModel, bindingContext);
    }
}