{extends file="app/Page.tpl"}
{block name='title'}Cashgame Leaderboard{/block}
{block name=nav}
	{partial view='core\Navigation\Navigation' model=$model->cashgamePageNavModel}
	{partial view='core\Navigation\Navigation' model=$model->cashgameYearNavModel}
{/block}
{block name=full}
	<div class="block gutter">
		<h1>
			{partial view='app\Cashgame\CashgameNavigation' model=$model->cashgameNavModel}
		</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Cashgame\Leaderboard\Table' model=$model->tableModel}
	</div>
{/block}