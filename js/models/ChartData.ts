import { ChartColumn } from './ChartColumn';
import { ChartRow } from './ChartRow';

export interface ChartData {
  colors: string[] | null;
  cols: ChartColumn[];
  rows: ChartRow[];
  p: null;
}

export interface NewChartData {
  labels: string[];
  datasets: NewChartDataset[];
}

export interface NewChartDataset {
  data: (number | null)[];
}
