{extends file="app/Page.tpl"}
{block name='title'}Running Cashgame{/block}
{block name=page}
	<div class="block gutter">
		<h1>Running Cashgame</h1>
	</div>

	<div class="button-list">
		{if $model->reportButtonEnabled}
			<a href="{$model->reportUrl->url}" class="icon-button action">
				<i class="icon-reorder"></i>
				Report
			</a>
		{/if}

		{if $model->buyinButtonEnabled}
			<a href="{$model->buyinUrl->url}" class="icon-button action">
				<i class="icon-money"></i>
				Buy In
			</a>
		{/if}

		{if $model->cashoutButtonEnabled}
			<a href="{$model->cashoutUrl->url}" class="icon-button action">
				<i class="icon-signout"></i>
				Cash Out
			</a>
		{/if}

		{if $model->endGameButtonEnabled}
			<a href="{$model->endGameUrl->url}" class="icon-button action">
				<i class="icon-signout"></i>
				End Game
			</a>
		{/if}
	</div>

	<div class="block gutter">

		{if $model->showTable}
			{partial view='app\Cashgame\Running\StatusTable' model=$model->statusTableModel}
		{else}
			No one has joined the game yet.
		{/if}

		{if $model->showChart}
			<div data-require="cashgame-game-chart" data-url="{$model->chartDataUrl->url}">Loading chart...</div>
		{/if}

	</div>
{/block}
{block name=aside2}
	<div class="block gutter">
		<dl class="cashgame-meta">
			{if $model->showStartTime}
				<dt>Start Time</dt>
				<dd>{$model->startTime}</dd>
			{/if}
			<dt>Location</dt>
			<dd>{$model->location}</dd>
		</dl>
	</div>
{/block}