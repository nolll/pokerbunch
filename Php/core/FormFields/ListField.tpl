<input type="text" name="{$model->fieldName}"{if isset($model->fieldId)} id="{$model->fieldId}"{/if} value="{$model->value}" list="{$model->listName}" class="textfield">
<datalist id="{$model->listName}">
	<label for="{$model->dropdownName}">or choose one</label>
	<select id="{$model->dropdownName}" name="{$model->dropdownName}">
		{html_options output=$model->names values=$model->values selected=$model->value}
	</select>
</datalist>