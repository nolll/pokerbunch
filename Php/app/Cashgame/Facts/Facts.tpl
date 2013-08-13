{extends file="app/Page.tpl"}
{block name='title'}Cashgame Facts{/block}
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
		<dl>
			<dt>Number of games</dt>
			<dd>{$model->gameCount}</dd>

			<dt>Best Result</dt>
			<dd>{$model->bestResultName}: {$model->bestResultAmount}</dd>

			<dt>Worst Result</dt>
			<dd>{$model->worstResultName}: {$model->worstResultAmount}</dd>

			<dt>Total Time Played</dt>
			<dd>{$model->totalGameTime}</dd>

			<dt>Most Time Played</dt>
			<dd>{$model->mostTimeName}: {$model->mostTimeDuration}</dd>

			<!--
			BiggestTotalBuyin
			BiggestTotalCashout
			BestTotalResult
			BiggestBuyin
			BiggestCashout
			BiggestComeback
			MostGamesPlayed
			HighestWinrate
			-->
		</dl>
	</div>
{/block}