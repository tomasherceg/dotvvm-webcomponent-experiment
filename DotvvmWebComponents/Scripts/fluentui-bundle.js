import * as fluentUiComponents from '@fluentui/web-components';
import * as fastAttr from "./binding-handlers/fast-attr"
import * as fastOptions from "./binding-handlers/fast-options"
import * as fastRowsData from "./binding-handlers/fast-rowsData"

fastAttr.init();
fastOptions.init();
fastRowsData.init();

export default fluentUiComponents;