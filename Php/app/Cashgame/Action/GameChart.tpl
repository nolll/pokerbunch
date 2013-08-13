<div data-require="cashgame-game-chart">Loading chart...</div>
<script>
	function getGameChartData() {ldelim}
		var data = new google.visualization.DataTable();
		data.addColumn( {ldelim} type: 'datetime', label: 'Time', pattern: 'HH:mm' {rdelim} );
		data.addColumn( {ldelim} type: 'number', label: 'Player 1' {rdelim} );
		data.addColumn( {ldelim} type: 'number', label: 'Player 2' {rdelim} );
		data.addColumn( {ldelim} type: 'number', label: 'Player 3' {rdelim} );

		data.addRows(5);

		data.setCell(0, 0, new Date('2013-04-08T21:00:00+02:00'));
		data.setCell(0, 1, 0);
		data.setCell(0, 2, 0);
		data.setCell(0, 3, 0);

		data.setCell(1, 0, new Date('2013-04-08T21:30:00+02:00'));
		data.setCell(1, 1, 200);
		data.setCell(1, 2, 200);
		data.setCell(1, 3, -100);

		data.setCell(2, 0, new Date('2013-04-08T22:00:00+02:00'));
		data.setCell(2, 1, 400);
		//data.setCell(2, 2, 350);
		data.setCell(2, 3, -150);

		data.setCell(3, 0, new Date('2013-04-08T22:30:00+02:00'));
		data.setCell(3, 1, 300);
		//data.setCell(3, 2, 350);
		data.setCell(3, 3, -200);

		data.setCell(4, 0, new Date('2013-04-08T23:00:00+02:00'));
		data.setCell(4, 1, 100);
		data.setCell(4, 2, 500);
		data.setCell(4, 3, -400);

		console.log(data.toJSON());

		return data;
	{rdelim}
</script>