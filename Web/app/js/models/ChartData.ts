import { ChartColumn } from './ChartColumn';
import { ChartRow } from './ChartRow';

export interface ChartData {
  colors: string[];
  cols: ChartColumn[];
  rows: ChartRow[];
  p: null;
}
