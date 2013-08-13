<select name="{$model->fieldName}"{if isset($model->fieldId)} id="{$model->fieldId}"{/if}>
	{html_options output=$model->names values=$model->values selected=$model->value}
</select>