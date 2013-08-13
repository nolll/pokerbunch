<div class="leaderboard list">
    {foreach item=itemModel from=$model->itemModels}
        {partial view='app\Cashgame\Leaderboard\Item' model=$itemModel}
	{/foreach}
</div>