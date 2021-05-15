export function init() {
    ko.bindingHandlers["fast-rowsData"] = {
        init(element, valueAccessor, allBindings, viewModel, bindingContext) {
        },
        update(element, valueAccessor, allBindings, viewModel, bindingContext) {
            const value = valueAccessor();

            const array = dotvvm.evaluator.getDataSourceItems(value);
            if (Array.isArray(array)) {
                element.rowsData = array.map(i => {
                    let item = dotvvm.serialization.serialize(i, { serializeAll: true });
                    if (typeof item === "object" && "$type" in item) {
                        delete item["$type"];
                    }
                    return item;
                });
            }
        }
    }
}
