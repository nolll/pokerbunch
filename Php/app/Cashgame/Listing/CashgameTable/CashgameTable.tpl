<table class="cashgames">
    <thead>
    <tr>
        <th class="title date">Date</th>
        <th class="title">Players</th>
        <th class="title">Location</th>
        <th class="title">Duration</th>
        <th class="title">Turnover</th>
        <th class="title">Average Buyin</th>
    </tr>
    </thead>
    <tbody>
		{foreach $model->listItemModels as $listItemModel}
			{partial view='app\Cashgame\Listing\CashgameTableItem\CashgameTableItem' model=$listItemModel}
		{/foreach}
    </tbody>
</table>