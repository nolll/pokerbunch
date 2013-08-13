{extends file="app/Page.tpl"}
{block name='title'}Create Homegame{/block}
{block name=full}
	<div class="block gutter">
		<h1>Join Homegame</h1>
	</div>
	<div class="block gutter">
		<p>
			Please enter your invitation code below
		</p>

		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<fieldset>
				<p>
					<label>Invitation Code</label>
					<input type="text" name="invitationcode" value="{$model}" class="longfield"/>
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Join</button>
			</div>
		</form>
	</div>
{/block}