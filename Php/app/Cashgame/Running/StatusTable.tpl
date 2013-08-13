{if !empty($model->statusModels)}
	<div class="standings">
		<div>
			{foreach $model->statusModels as $statusModel}
				{partial view='app\Cashgame\Running\StatusItem' model=$statusModel}
			{/foreach}
		</div>

		<div class="totals">
			<div class="title">Totals: </div>
			<div class="amounts">
				<div class="amount"><i title="Total Buy in" class="icon-signin"></i> {$model->totalBuyin}</div>
				<div class="amount"><i title="Total Stacks" class="icon-reorder"></i> {$model->totalStacks}</div>
			</div>
		</div>

	</div>

{/if}