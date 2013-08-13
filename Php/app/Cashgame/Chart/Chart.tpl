{extends file="app/Page.tpl"}
{block name='title'}Cashgame Chart{/block}
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
		<div data-require="cashgame-chart" data-url="{$model->chartDataUrl->url}">Loading chart...</div>
	</div>
{/block}