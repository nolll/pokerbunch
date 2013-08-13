{if isset($model->nodes) and count($model->nodes) gt 0}
	<nav class="{$model->cssClass}"{if isset($model->dataRequire)} data-require="{$model->dataRequire}"{/if}>
		{if $model->headingIsLinked}
			<h2><a href="{$model->headingLink->url}">{$model->heading}</a></h2>
		{else}
			<h2>{$model->heading}</h2>
		{/if}
		<ul>
			{foreach $model->nodes as $node}
				<li{if $node->selected} class="selected"{/if}><a href="{$node->urlModel->url}"><span>{$node->name}</span></a></li>
			{/foreach}
		</ul>
	</nav>
{/if}