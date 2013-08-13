{extends file="app/Page.tpl"}
{block name='title'}Edit Cashgame{/block}
{block name=full}
	<div class="block gutter">
		<h1>Edit Cashgame</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<fieldset>
				<p>
					{$model->isoDate}
				</p>
				<p>
					<label>Location</label>
					{partial view='core\FormFields\ListField' model=$model->locationSelectModel}
				</p>
			</fieldset>
			<div class="buttons">
				<button type="submit" class="action">Save Changes</button>
				{if $model->enableDelete}
					<a href="{$model->deleteUrl->url}" class="button warning" data-require="delete-confirmation" data-message="Delete Cashgame?">Delete</a>
				{/if}
				<a href="{$model->cancelUrl->url}" class="button">Cancel</a>
			</div>
		</form>
	</div>
{/block}