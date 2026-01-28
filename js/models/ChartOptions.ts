export interface ChartOptions {
  pointSize: number;
  vAxis?:
    | {
        minValue: number;
        textPosition?: string;
        gridlines?: {
          color: string;
        };
        baselineColor?: string;
        viewWindowMode?: string;
        viewWindow?: {
          max: number;
          min: number;
        };
      }
    | undefined;
  hAxis?:
    | {
        format: string;
        textPosition?: string;
        gridlines?: {
          color: string;
        };
        baselineColor?: string;
      }
    | undefined;
  legend?: { position: string } | undefined;
  colors?: string[] | undefined;
  series?: { 1: { type: 'area' } } | undefined;
  tooltip?: { trigger: string } | undefined;
  enableInteractivity?: boolean;
}

export interface NewChartOptions {
  responsive: Boolean;
}
