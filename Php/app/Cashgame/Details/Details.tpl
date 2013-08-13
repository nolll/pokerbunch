{extends file="app/Page.tpl"}
{block name='title'}Cashgame{/block}
{block name=full}
	<div class="block gutter">
		<h1>{$model->heading}</h1>
	</div>
{/block}
{block name=aside2}
	<div class="block gutter">
		<dl class="cashgame-meta">
			<dt>Status</dt>
			<dd>{$model->status}</dd>
			{if $model->showStartTime}
				<dt>Start Time</dt>
				<dd>{$model->startTime}</dd>
			{/if}
			{if $model->showEndTime}
				<dt>End Time</dt>
				<dd>{$model->endTime}</dd>
			{/if}
			{if $model->durationEnabled}
				<dt>Duration</dt>
				<dd>{$model->duration}</dd>
			{/if}
			<dt>Location</dt>
			<dd>{$model->location}</dd>
		</dl>

		{if $model->enableEdit}
			<p>
				<a href="{$model->editUrl->url}" class="button">Edit Cashgame</a>
			</p>
		{/if}
	</div>
{/block}
{block name=page}
	<div class="block gutter">
		{partial view='app\Cashgame\Details\ResultTable\ResultTable' model=$model->resultTableModel}

		<div data-require="cashgame-game-chart" data-url="{$model->chartDataUrl->url}">Loading chart...</div>

		<div class="buttons">
			{if $model->enableCheckpointsButton}
				<a href="{$model->checkpointsUrl->url}" class="button action">My Details</a>
			{/if}

			{if $model->enablePlayerButton}
				<a href="{$model->playerUrl->url}" class="button action">My Details</a>
			{/if}
		</div>

	</div>
{/block}