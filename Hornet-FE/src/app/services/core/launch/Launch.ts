import { LaunchServiceProvider } from "./LaunchServiceProvider";
import { LaunchStatus } from "./LaunchStatus";
import { Rocket } from "./Rocket";

export interface Launch {
  id: string; 
  name: string;
  slug?: string;
  launchStatus: LaunchStatus;
  windowStart?: DateString;
  windowEnd?: DateString;
  imageUrl?: string;
  net?: DateString;
  rocket: Rocket;
  launchServiceProvider: LaunchServiceProvider;
}