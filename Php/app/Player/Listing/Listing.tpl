{extends file="app/Page.tpl"}
{block name='title'}Player List{/block}
{block name=full}
	<div class="block gutter">
		<h1>Players</h1>
	</div>
	<div class="block gutter">

		<ul class="players simple list">
			{foreach $model->playerModels as $playerModel}
				<li>
					{partial view='app\Player\Listing\Item' model=$playerModel}
				</li>
			{/foreach}
		</ul>

		{if $model->showAddLink}
			<div class="add-player-button">
				<a href="{$model->addUrl->url}" class="button">Add Player</a>
			</div>
		{/if}
	</div>
{/block}