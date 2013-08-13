<h2>Player Achievements</h2>

<div class="achievements">
    <ul>
		{if $model->playedOneGame}
			<li>Played a game</li>
		{/if}
		{if $model->playedTenGames}
			<li>Played ten games</li>
		{/if}
		{if $model->played50Games}
			<li>Played 50 games</li>
		{/if}
		{if $model->played100Games}
			<li>Played 100 games</li>
		{/if}
		{if $model->played200Games}
			<li>Played 200 games</li>
		{/if}
		{if $model->played500Games}
			<li>Played 500 games</li>
		{/if}
    </ul>
</div>