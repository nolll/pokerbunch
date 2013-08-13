{extends file="app/Page.tpl"}
{block name='title'}Add Player{/block}
{block name=full}
	<div class="block gutter">
		<h1>Invite Player</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}

		<p>
			Invite this player to see his own results.
		</p>

		<form method="post">
			<fieldset>
				<p>
					<label>Email</label>
					<input type="text" name="email" value="{$email}" class="textfield"/>
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Invite</button>
			</div>
		</form>
	</div>
{/block}