{extends file="app/Page.tpl"}
{block name='title'}Player Added{/block}
{block name=full}
	<div class="block gutter">
		<h1>Player Added</h1>
	</div>
	<div class="block gutter">
		<p>
			The player was added to {$model->homegameName}.
		</p>
	</div>
{/block}