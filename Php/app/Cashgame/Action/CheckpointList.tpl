<ul class="checkpoint-list">
	{foreach $model as $checkpoint}
		<li class="checkpoint">
			{partial view='app\Cashgame\Action\CheckpointItem' model=$checkpoint}
		</li>
	{/foreach}
</ul>