<nav class="game-nav">
	<h2><a href="{$model->headingLink->url}">{$model->heading}</a></h2>
	<ul>
		<li>
			<a href="{$model->cashgameLink->url}"><span>Cashgames</span></a>
			{if $model->cashgameIsRunning}
				<a href="{$model->runningLink->url}" class="running-game-link icon-cogs" title="Running Game"><span>Running Game</span></a>
			{else}
				<a href="{$model->createLink->url}" class="start-game-link icon-plus" title="Add Game"><span>Start Game</span></a>
			{/if}
		</li>
		<li>
			<a href="{$model->playerLink->url}"><span>Players</span></a>
		</li>
	</ul>
</nav>