{extends file="app/Page.tpl"}
{block name='title'}Homegame List{/block}
{block name=full}
	<div class="block gutter">
		<h1>Homegames</h1>
	</div>
	<div class="block gutter">

		<ul class="homegames  simple list">
			{foreach $model->homegameModels as $homegameModel}
				<li>
					{partial view='app\Homegame\Listing\Item' model=$homegameModel}
				</li>
			{/foreach}
		</ul>
	</div>
{/block}