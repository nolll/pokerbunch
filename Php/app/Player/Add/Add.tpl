{extends file="app/Page.tpl"}
{block name='title'}Add Player{/block}
{block name=full}
	<div class="block gutter">
		<h1>Add Player</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<fieldset>
				<p>
					<label>Player Name</label>
					<input type="text" name="name" value="{$name}" class="textfield"/>
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Add</button>
			</div>
		</form>
	</div>
{/block}