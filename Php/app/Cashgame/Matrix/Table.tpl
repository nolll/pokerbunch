<div class="matrix-container">
    <table class="cashgames matrix">
        <thead>
		<tr>
			<th class="title">Rank</th>
			<th class="title">Player</th>
			<th class="title">Winnings</th>
			{foreach item=columnHeaderModel from=$model->columnHeaderModels}
				{partial view='app\Cashgame\Matrix\ColumnHeader' model=$columnHeaderModel}
			{/foreach}
		</tr>
        </thead>

        <tbody>
			{foreach item=rowModel from=$model->rowModels}
				{partial view='app\Cashgame\Matrix\Row' model=$rowModel}
			{/foreach}
        </tbody>

    </table>
</div>