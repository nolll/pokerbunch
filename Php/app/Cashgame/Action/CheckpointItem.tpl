{if $model->showLink}
	<a href="{$model->editUrl->url}" data-require="delete-confirmation" data-message="Delete checkpoint?">{$model->timestamp}</a>
{else}
	{$model->timestamp}
{/if}
{$model->description}: {$model->stack}