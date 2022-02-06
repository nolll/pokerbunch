import { ChartColumn } from './ChartColumn';
import { ChartRow } from './ChartRow';

export interface ChartData {
  colors: string[] | null;
  cols: ChartColumn[];
  rows: ChartRow[];
  p: null;
}
