{extends file="app/Page.tpl"}
{block name='title'}New Cashgame{/block}
{block name=nav}
	{partial view='core\Navigation\Navigation' model=$model->cashgamePageNavModel}
	{partial view='core\Navigation\Navigation' model=$model->cashgameYearNavModel}
{/block}
{block name=full}
	<div class="block gutter">
		<h1>New Cashgame</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<fieldset>
				<p>
					<label for="location">Enter Location</label>
					{partial view='core\FormFields\ListField' model=$model->locationSelectModel}
				</p>
			</fieldset>
			<div class="buttons">
				<button type="submit" class="action">Create</button>
			</div>
		</form>
	</div>
{/block}