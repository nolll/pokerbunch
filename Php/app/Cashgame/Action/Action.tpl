{extends file="app/Page.tpl"}
{block name='title'}Player Actions{/block}
{block name=nav}
	{partial view='core\Navigation\Navigation' model=$model->cashgamePageNavModel}
	{partial view='core\Navigation\Navigation' model=$model->cashgameYearNavModel}
{/block}
{block name=page}
	<div class="block gutter">
		<h1>{$model->heading}</h1>
	</div>
	<div class="block gutter">
		<div>
			<div data-require="cashgame-action-chart" data-url="{$model->chartDataUrl->url}">Loading chart...</div>
		</div>
	</div>
{/block}
{block name=aside2}
	<div class="block gutter">
		{partial view='app\Cashgame\Action\CheckpointList' model=$model->checkpoints}
	</div>
{/block}