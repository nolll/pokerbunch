{extends file="app/Page.tpl"}
{block name='title'}Homegame Details{/block}
{block name=full}
	<div class="block gutter">
		<h1>{$model->displayName}</h1>
	</div>
	<div class="block gutter">
		<p>
			{$model->description}
		</p>
		{if isset($model->houseRules)}
			<h2>House Rules</h2>
			<p>
				{$model->houseRules}
			</p>
		{/if}
		{if $model->showEditLink}
			<p>
				<a href="{$model->editUrl->url}">Edit Game</a>
			</p>
		{/if}
	</div>
{/block}