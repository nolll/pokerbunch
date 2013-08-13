<div class="standings-item">
    <div class="name">
		<a href="{$model->playerUrl->url}">{$model->name}</a>
		{if $model->hasCashedOut}
			<i title="Cashed out" class="icon-ok-sign"></i>
		{/if}
	</div>
	<div class="amounts">
		<div><i title="Buy in" class="icon-signin"></i> {$model->buyin}</div>
		<div><i title="Stack" class="icon-reorder"></i> {$model->stack}</div>
		<div class="{$model->winningsClass}">{$model->winnings}</div>
	</div>
	<div class="time"><i title="Last report" class="icon-time"></i> {$model->time}</div>
	{if $model->managerButtonsEnabled}
		<div class="controls">
			<div class="control"><a href="{$model->buyinUrl->url}"><i title="Buy In" class="icon-money"></i></a></div>
			<div class="control"><a href="{$model->reportUrl->url}"><i title="Report Stack" class="icon-pushpin"></i></a></div>
			<div class="control"><a href="{$model->cashoutUrl->url}"><i title="Cash Out" class="icon-signout"></i></a></div>
		</div>
	{/if}
</div>