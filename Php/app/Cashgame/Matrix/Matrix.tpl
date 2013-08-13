{extends file="app/Page.tpl"}
{block name='title'}Cashgame Matrix{/block}
{block name=full}
	<div class="block gutter">
		<h1>
			{partial view='app\Cashgame\CashgameNavigation' model=$model->cashgameNavModel}
		</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Cashgame\Matrix\Table' model=$model->tableModel}
	</div>
{/block}